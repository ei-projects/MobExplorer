using System;
using System.Collections.Generic;
using System.Text;

namespace MobLib
{
    public class MobFileSection
    {
        public byte[] Data;
        public MobFileSectionInfo info;
        //public int StdIndex;
        public List<MobFileSection> Items;
        public MobFileSection Owner;
        public bool Readed;
        public object UserValue; // Адовый костыль. Здесь будут храниться всякие ГУИшные данные

        public MobFileSection(MobFileSection owner = null)
        {
            this.Owner = owner;
            Data = null;
            //data_size = 0;
            info = null;
            Items = null;
            Readed = false;
        }
        public ErrorCodes ReadSubsections()
        {
            if (Readed) return ErrorCodes.OK;
            if (info != null && info.Type != SectionType.ST_REC) return ErrorCodes.InvalidSection;
            uint tmpsize, id, size; //data_size;
            int curofs, i; //data_offset;
            if (info == null) { curofs = 0; size = (uint)Data.Length; }
            else { curofs = 8; size = (uint)Data.Length - 8; }

            Items = new List<MobFileSection>();
            while (size > 0)
            {
                id = BitConverter.ToUInt32(Data, curofs);
                tmpsize = BitConverter.ToUInt32(Data, curofs + 4);
                Items.Add(new MobFileSection(this));

                for (i = 1; i < MobFile.SectionInfos.Length; ++i) // begin from 1 for ignore ID_UNKNOWN
                {
                    if (Convert.ToUInt32(MobFile.SectionInfos[i].Id) == id)
                    {
                        Items[Items.Count - 1].info = MobFile.SectionInfos[i];
                        break;
                    }
                }
                if (i == MobFile.SectionInfos.Length)
                    Items[Items.Count - 1].info = MobFile.SectionInfos[0];

                if (tmpsize > size)
                    tmpsize = size;
                Items[Items.Count - 1].Data = new byte[tmpsize];
                Array.Copy(Data, curofs, Items[Items.Count - 1].Data, 0, tmpsize);

                //items[items.Count - 1].data = data.//data;
                //items[items.Count - 1].data_offset = curofs + 8;
                //items[items.Count - 1].data_size = tmpsize - 8;

                curofs += (int)tmpsize;
                size -= tmpsize;
            }
            byte[] tempdata = Data;
            if (info == null)
                Data = null;
            else
                Data = new byte[8];
            if (Data != null)
                Array.Copy(tempdata, Data, 8);
            tempdata = null;
            Readed = true;
            return ErrorCodes.OK;
        }
        public MobFileSection Clone(MobFileSection owner)
        {
            MobFileSection new_sect = new MobFileSection(owner);
            Stack<MobFileSection> stack1 = new Stack<MobFileSection>();
            Stack<MobFileSection> stack2 = new Stack<MobFileSection>();

            MobFileSection cur = this, cur_new = new_sect;
            stack1.Push(null);
            stack2.Push(null);

            do
            {
                if (cur.Items != null && cur.Items.Count > 0)
                {
                    cur_new.Items = new List<MobFileSection>(cur.Items.Count);
                    for (int i = 0; i < cur.Items.Count; ++i)
                    {
                        stack1.Push(cur.Items[i]);
                        cur_new.Items.Add(new MobFileSection(cur_new));
                        stack2.Push(cur_new.Items[i]);
                    }
                }
                else cur_new.Items = null;

                if (cur.Data != null)
                    cur_new.Data = (byte[])cur.Data.Clone(); // WARNING
                else
                    cur_new.Data = null;
                cur_new.info = cur.info;
                cur_new.Readed = cur.Readed;

                cur = stack1.Pop();
                cur_new = stack2.Pop();
            } while (cur != null && cur_new != null);

            return new_sect;
        }
        public int FixSize(int delta) // delta = newSize - oldSize
        {
            MobFileSection cur = this;
            int size;
            while (cur != null)
            {
                if (cur.Data != null)
                    if (cur.Data.Length >= 8)
                    {
                        size = BitConverter.ToInt32(cur.Data, 4);
                        size += delta;
                        BitConverter.GetBytes(size).CopyTo(cur.Data, 4);
                    }
                    else return 1;
                cur = cur.Owner;
            }
            return 0;
        }
        public static void CryptScript(byte[] src)
        {
            uint tmpKey, i, key = BitConverter.ToUInt32(src, 8);
            for (i = 12; i < src.Length; ++i)
            {
                tmpKey = ((((key * 13) << 4) + key) << 8) - key;
                key += (tmpKey << 2) + 2531011;
                tmpKey = key >> 16;
                src[i] ^= (byte)tmpKey;
            }
        }
        public void setString(string text)
        {
            MobFileSection cur = this;
            var encoding = Encoding.GetEncoding(1251);
            MobFileSectionInfo info = cur.info;
            byte[] temp;
            int delta = BitConverter.ToInt32(cur.Data, 4) - 8;

            switch (info.Type)
            {
                case SectionType.ST_STRING:
                case SectionType.ST_SCRIPT:
                    temp = new byte[text.Length + 8];
                    Array.Copy(cur.Data, temp, 8);
                    encoding.GetBytes(text).CopyTo(temp, 8);
                    break;

                case SectionType.ST_SCRIPT_ENC:
                    delta -= 4;
                    temp = new byte[text.Length + 12];
                    Array.Copy(cur.Data, temp, 12);
                    encoding.GetBytes(text).CopyTo(temp, 12);
                    CryptScript(temp);
                    break;
                default:
                    return;
            }

            cur.Data = temp;
            delta = text.Length - delta;
            FixSize(delta);

        }
        public string toString()
        {
            MobFileSection cur = this;
            var encoding = Encoding.GetEncoding(1251);
            MobFileSectionInfo info = cur.info;
            int delta = 0;
            string result = "";

            switch (info.Type)
            {
                case SectionType.ST_STRING:
                case SectionType.ST_SCRIPT:
                    delta = BitConverter.ToInt32(cur.Data, 4) - 8;
                    result = encoding.GetString(cur.Data, 8, delta);
                    break;

                case SectionType.ST_SCRIPT_ENC:
                    delta = BitConverter.ToInt32(cur.Data, 4) - 12;
                    CryptScript(cur.Data);
                    result = encoding.GetString(cur.Data, 12, delta);
                    CryptScript(cur.Data);
                    break;
            }

            return result;
        }
        public double toNumber()
        {
            MobFileSection sec = this;
            switch (sec.info.Type)
            {
                case SectionType.ST_BYTE:
                    return sec.Data[8];

                case SectionType.ST_DWORD:
                    return BitConverter.ToUInt32(sec.Data, 8);

                case SectionType.ST_FLOAT:
                    return BitConverter.ToSingle(sec.Data, 8);
            }

            return double.NaN;
        }
        public void setNumber(double value)
        {
            MobFileSection sec = this;
            switch (sec.info.Type)
            {
                case SectionType.ST_BYTE:
                    sec.Data[8] = Convert.ToByte(value);
                    break;

                case SectionType.ST_DWORD:
                    BitConverter.GetBytes(Convert.ToUInt32(value)).CopyTo(sec.Data, 8);
                    break;

                case SectionType.ST_FLOAT:
                    BitConverter.GetBytes(Convert.ToSingle(value)).CopyTo(sec.Data, 8);
                    break;
            }
        }
        public void setId(SectionId id)
        {
            MobFileSection sec = this;
            sec.info.Id = id;
            BitConverter.GetBytes(Convert.ToUInt32(id)).CopyTo(sec.Data, 0);
        }
    }
}
