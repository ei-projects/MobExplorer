using System;
using System.Collections.Generic;
using System.Text;

namespace MobLib
{
    public class MobHelper
    {
        public static bool openSectionById(MobFile mob, SectionId id)
        {
            List<MobFileSection> items = mob.CurrentSection.Items;
            for (int i = 0; i < items.Count; ++i)
            {
                if (items[i].info.Id == id)
                {
                    mob.CurrentSection = items[i];
                    mob.CurrentSection.ReadSubsections();
                    return true;
                }
            }

            return false;
        }
    }

    public class MobFileObjectInfo
    {
        public string Name;
        public string Prototype;
        public string Id;
        public MobFileObjectInfo()
        {
            Name = "";
            Prototype = "";
            Id = "";
        }
    }

    public struct MobUnitStats
    {
        public uint unk1;
        public uint unk2;
        public uint unk3;
        public uint unk4;
        public float unk5;
        public float actions;
        public float unk6;
        public float unk7;
        public float unk8;
        public float unk9;
        public float unk10;
        public float unk11;
        public float unk12;
        public float unk13;
        public uint unk14;
        public uint unk15;
        public float unk16;
        public float unk17;
        public float unk18;
        public float unk19;
        public float unk20;
        public uint unk21;
        public uint unk22;
        public uint unk23;
        public uint unk24;
        public uint unk25;
        public uint unk26;
        public uint unk27;
        public uint unk28;
        public float unk29;
        public float unk30;
        public float unk31;
        public float unk32;
        public float unk33;
        public float unk34;
        public float unk35;
        public float unk36;
        public float unk37;
        public float unk38;
        public float unk39;
        public float unk40;
        public uint unk41;
        public uint unk42;
    }
}
