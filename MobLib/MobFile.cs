using System;
using System.Collections.Generic;

namespace MobLib
{

    public enum ErrorCodes
    {
        OK,
        Unknown,
        CannotOpen,
        InvalidSection
    };
    public enum SectionType
    {
        ST_NULL = 0x0,
        ST_REC = 0x1,
        ST_STRING = 0x2,
        ST_PLOT = 0x3,
        ST_QUATERNION = 0x4,
        ST_DWORD = 0x5,
        ST_DIPLOMACY = 0x6,
        ST_STR_SAR = 0x7,
        ST_FLOAT = 0x8,
        ST_BYTE = 0x9,
        ST_STR_S01 = 0xA,
        ST_STR_S06 = 0xB,
        ST_STR_S02 = 0xC,
        ST_BINARY = 0xD,
        ST_SCRIPT = 0xE,
        ST_SCRIPT_ENC = 0xF,
        ST_STR_S08 = 0x10,
        ST_RECT = 0x11,
        ST_UNK = -1
    }

    public enum SectionId : uint
    {
        ID_UNKNOWN = 0xFFFFFFFF,
        ID_WORLD_SET = 0x0000ABD0,
        ID_OBJ_DEF_LOGIC = 0x0000B010,
        ID_PR_OBJECTDBFILE = 0x0000D000,
        ID_DIR_NAME = 0x0000E002,
        ID_DIPLOMATION = 0xDDDDDDD1,
        ID_WS_WIND_DIR = 0x0000ABD1,
        ID_OBJECTSECTION = 0x0000B000,
        ID_OBJROTATION = 0x0000B00A,
        ID_OBJ_PLAYER = 0x0000B011,
        ID_DIR_NINST = 0x0000E003,
        ID_DIPLOMATION_FOF = 0xDDDDDDD2,
        ID_OBJECTDBFILE = 0x0000A000,
        ID_LIGHT_SECTION = 0x0000AA00,
        ID_WS_WIND_STR = 0x0000ABD2,
        ID_OBJECT = 0x0000B001,
        ID_OBJTEXTURE = 0x0000B00B,
        ID_OBJ_PARENT_ID = 0x0000B012,
        ID_SOUND_SECTION = 0x0000CC00,
        ID_SOUND_RESNAME = 0x0000CC0A,
        ID_PARTICL_SECTION = 0x0000DD00,
        ID_DIR_PARENT_FOLDER = 0x0000E004,
        ID_SEC_RANGE = 0x0000FF00,
        ID_DIPLOMATION_PL_NAMES = 0xDDDDDDD3,
        ID_LIGHT = 0x0000AA01,
        ID_WS_TIME = 0x0000ABD3,
        ID_NID = 0x0000B002,
        ID_OBJCOMPLECTION = 0x0000B00C,
        ID_OBJ_USE_IN_SCRIPT = 0x0000B013,
        ID_SOUND = 0x0000CC01,
        ID_SOUND_RANGE2 = 0x0000CC0B,
        ID_PARTICL = 0x0000DD01,
        ID_DIR_TYPE = 0x0000E005,
        ID_MAIN_RANGE = 0x0000FF01,
        ID_VSS_BS_COMMANDS = 0x00001E10,
        ID_LIGHT_RANGE = 0x0000AA02,
        ID_WS_AMBIENT = 0x0000ABD4,
        ID_OBJTYPE = 0x0000B003,
        ID_OBJBODYPARTS = 0x0000B00D,
        ID_OBJ_IS_SHADOW = 0x0000B014,
        ID_SOUND_ID = 0x0000CC02,
        ID_PARTICL_ID = 0x0000DD02,
        ID_RANGE = 0x0000FF02,
        ID_VSS_SECTION = 0x00001E00,
        ID_VSS_ISSTART = 0x00001E0A,
        ID_VSS_CUSTOM_SRIPT = 0x00001E11,
        ID_LIGHT_NAME = 0x0000AA03,
        ID_WS_SUN_LIGHT = 0x0000ABD5,
        ID_OBJNAME = 0x0000B004,
        ID_PARENTTEMPLATE = 0x0000B00E,
        ID_OBJ_R = 0x0000B015,
        ID_SOUND_POSITION = 0x0000CC03,
        ID_SOUND_AMBIENT = 0x0000CC0D,
        ID_PARTICL_POSITION = 0x0000DD03,
        ID_UNIT = 0xBBBB0000,
        ID_UNIT_NEED_IMPORT = 0xBBBB000A,
        ID_VSS_TRIGER = 0x00001E01,
        ID_VSS_LINK = 0x00001E0B,
        ID_LIGHT_POSITION = 0x0000AA04,
        ID_OBJINDEX = 0x0000B005,
        ID_OBJCOMMENTS = 0x0000B00F,
        ID_OBJ_QUEST_INFO = 0x0000B016,
        ID_SOUND_RANGE = 0x0000CC04,
        ID_SOUND_IS_MUSIC = 0x0000CC0E,
        ID_PARTICL_COMMENTS = 0x0000DD04,
        ID_MAGIC_TRAP = 0xBBAB0000,
        ID_UNIT_R = 0xBBBB0001,
        ID_UNIT_LOGIC = 0xBBBC0000,
        ID_UNIT_LOGIC_WAIT = 0xBBBC000A,
        ID_VSS_CHECK = 0x00001E02,
        ID_VSS_GROUP = 0x00001E0C,
        ID_LIGHT_ID = 0x0000AA05,
        ID_OBJTEMPLATE = 0x0000B006,
        ID_SOUND_NAME = 0x0000CC05,
        ID_PARTICL_NAME = 0x0000DD05,
        ID_MIN_ID = 0x0000FF05,
        ID_MT_DIPLOMACY = 0xBBAB0001,
        ID_LEVER = 0xBBAC0000,
        ID_UNIT_PROTOTYPE = 0xBBBB0002,
        ID_UNIT_LOGIC_AGRESSIV = 0xBBBC0001,
        ID_UNIT_LOGIC_ALARM_CONDITION = 0xBBBC000B,
        ID_GUARD_PT = 0xBBBD0000,
        ID_VSS_PATH = 0x00001E03,
        ID_VSS_IS_USE_GROUP = 0x00001E0D,
        ID_LIGHT_SHADOW = 0x0000AA06,
        ID_OBJPRIMTXTR = 0x0000B007,
        ID_SOUND_MIN = 0x0000CC06,
        ID_PARTICL_TYPE = 0x0000DD06,
        ID_MAX_ID = 0x0000FF06,
        ID_MT_SPELL = 0xBBAB0002,
        ID_LEVER_SCIENCE_STATS = 0xBBAC0001,
        ID_UNIT_ITEMS = 0xBBBB0003,
        ID_UNIT_LOGIC_CYCLIC = 0xBBBC0002,
        ID_UNIT_LOGIC_HELP = 0xBBBC000C,
        ID_GUARD_PT_POSITION = 0xBBBD0001,
        ID_ACTION_PT = 0xBBBE0000,
        ID_VSS_ID = 0x00001E04,
        ID_VSS_VARIABLE = 0x00001E0E,
        ID_LIGHT_COLOR = 0x0000AA07,
        ID_OBJSECTXTR = 0x0000B008,
        ID_SOUND_MAX = 0x0000CC07,
        ID_PARTICL_SCALE = 0x0000DD07,
        ID_AIGRAPH = 0x31415926,
        ID_MT_AREAS = 0xBBAB0003,
        ID_LEVER_CUR_STATE = 0xBBAC0002,
        ID_UNIT_STATS = 0xBBBB0004,
        ID_UNIT_LOGIC_MODEL = 0xBBBC0003,
        ID_UNIT_LOGIC_ALWAYS_ACTIVE = 0xBBBC000D,
        ID_GUARD_PT_ACTION = 0xBBBD0002,
        ID_ACTION_PT_LOOK_PT = 0xBBBE0001,
        ID_TORCH = 0xBBBF0000,
        ID_VSS_RECT = 0x00001E05,
        ID_VSS_BS_CHECK = 0x00001E0F,
        ID_LIGHT_COMMENTS = 0x0000AA08,
        ID_OBJPOSITION = 0x0000B009,
        ID_SOUND_COMMENTS = 0x0000CC08,
        ID_MT_TARGETS = 0xBBAB0004,
        ID_LEVER_TOTAL_STATE = 0xBBAC0003,
        ID_UNIT_QUEST_ITEMS = 0xBBBB0005,
        ID_UNIT_LOGIC_GUARD_R = 0xBBBC0004,
        ID_UNIT_LOGIC_AGRESSION_MODE = 0xBBBC000E,
        ID_ACTION_PT_WAIT_SEG = 0xBBBE0002,
        ID_TORCH_STRENGHT = 0xBBBF0001,
        ID_VSS_SRC_ID = 0x00001E06,
        ID_SOUND_VOLUME = 0x0000CC09,
        ID_MT_CAST_INTERVAL = 0xBBAB0005,
        ID_LEVER_IS_CYCLED = 0xBBAC0004,
        ID_UNIT_QUICK_ITEMS = 0xBBBB0006,
        ID_UNIT_LOGIC_GUARD_PT = 0xBBBC0005,
        ID_ACTION_PT_TURN_SPEED = 0xBBBE0003,
        ID_TORCH_PTLINK = 0xBBBF0002,
        ID_VSS_DST_ID = 0x00001E07,
        ID_LEVER_CAST_ONCE = 0xBBAC0005,
        ID_UNIT_SPELLS = 0xBBBB0007,
        ID_UNIT_LOGIC_NALARM = 0xBBBC0006,
        ID_ACTION_PT_FLAGS = 0xBBBE0004,
        ID_TORCH_SOUND = 0xBBBF0003,
        ID_VSS_TITLE = 0x00001E08,
        ID_LEVER_SCIENCE_STATS_NEW = 0xBBAC0006,
        ID_UNIT_WEAPONS = 0xBBBB0008,
        ID_UNIT_LOGIC_USE = 0xBBBC0007,
        ID_VSS_COMMANDS = 0x00001E09,
        ID_DIRICTORY_ELEMENTS = 0x0000F000,
        ID_LEVER_IS_DOOR = 0xBBAC0007,
        ID_UNIT_ARMORS = 0xBBBB0009,
        ID_UNIT_LOGIC_REVENGE = 0xBBBC0008,
        ID_DIRICTORY = 0x0000E000,
        ID_SS_TEXT_OLD = 0xACCEECCA,
        ID_LEVER_RECALC_GRAPH = 0xBBAC0008,
        ID_UNIT_LOGIC_FEAR = 0xBBBC0009,
        ID_SC_OBJECTDBFILE = 0x0000C000,
        ID_FOLDER = 0x0000E001,
        ID_SS_TEXT = 0xACCEECCB
    }
    public class MobFileSectionInfo
    {
        public SectionId Id;
        public SectionType Type;
    }
    public class MobFile
    {
        public static MobFileSectionInfo[] SectionInfos = new MobFileSectionInfo[] {
            new MobFileSectionInfo {Id = SectionId.ID_UNKNOWN, Type = SectionType.ST_UNK},
            new MobFileSectionInfo {Id = SectionId.ID_WORLD_SET, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_OBJ_DEF_LOGIC, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_PR_OBJECTDBFILE, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_DIR_NAME, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_DIPLOMATION, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_WS_WIND_DIR, Type = SectionType.ST_PLOT},
            new MobFileSectionInfo {Id = SectionId.ID_OBJECTSECTION, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_OBJROTATION, Type = SectionType.ST_QUATERNION},
            new MobFileSectionInfo {Id = SectionId.ID_OBJ_PLAYER, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_DIR_NINST, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_DIPLOMATION_FOF, Type = SectionType.ST_DIPLOMACY},
            new MobFileSectionInfo {Id = SectionId.ID_OBJECTDBFILE, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_LIGHT_SECTION, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_WS_WIND_STR, Type = SectionType.ST_FLOAT},
            new MobFileSectionInfo {Id = SectionId.ID_OBJECT, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_OBJTEXTURE, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_OBJ_PARENT_ID, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_SECTION, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_RESNAME, Type = SectionType.ST_STR_SAR},
            new MobFileSectionInfo {Id = SectionId.ID_PARTICL_SECTION, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_DIR_PARENT_FOLDER, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_SEC_RANGE, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_DIPLOMATION_PL_NAMES, Type = SectionType.ST_STR_SAR},
            new MobFileSectionInfo {Id = SectionId.ID_LIGHT, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_WS_TIME, Type = SectionType.ST_FLOAT},
            new MobFileSectionInfo {Id = SectionId.ID_NID, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_OBJCOMPLECTION, Type = SectionType.ST_PLOT},
            new MobFileSectionInfo {Id = SectionId.ID_OBJ_USE_IN_SCRIPT, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_RANGE2, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_PARTICL, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_DIR_TYPE, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_MAIN_RANGE, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_BS_COMMANDS, Type = SectionType.ST_STR_SAR},
            new MobFileSectionInfo {Id = SectionId.ID_LIGHT_RANGE, Type = SectionType.ST_FLOAT},
            new MobFileSectionInfo {Id = SectionId.ID_WS_AMBIENT, Type = SectionType.ST_FLOAT},
            new MobFileSectionInfo {Id = SectionId.ID_OBJTYPE, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_OBJBODYPARTS, Type = SectionType.ST_STR_SAR},
            new MobFileSectionInfo {Id = SectionId.ID_OBJ_IS_SHADOW, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_ID, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_PARTICL_ID, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_RANGE, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_SECTION, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_ISSTART, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_CUSTOM_SRIPT, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_LIGHT_NAME, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_WS_SUN_LIGHT, Type = SectionType.ST_FLOAT},
            new MobFileSectionInfo {Id = SectionId.ID_OBJNAME, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_PARENTTEMPLATE, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_OBJ_R, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_POSITION, Type = SectionType.ST_PLOT},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_AMBIENT, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_PARTICL_POSITION, Type = SectionType.ST_PLOT},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_NEED_IMPORT, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_TRIGER, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_LINK, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_LIGHT_POSITION, Type = SectionType.ST_PLOT},
            new MobFileSectionInfo {Id = SectionId.ID_OBJINDEX, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_OBJCOMMENTS, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_OBJ_QUEST_INFO, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_RANGE, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_IS_MUSIC, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_PARTICL_COMMENTS, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_MAGIC_TRAP, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_R, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_WAIT, Type = SectionType.ST_FLOAT},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_CHECK, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_GROUP, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_LIGHT_ID, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_OBJTEMPLATE, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_NAME, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_PARTICL_NAME, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_MIN_ID, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_MT_DIPLOMACY, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_LEVER, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_PROTOTYPE, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_AGRESSIV, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_ALARM_CONDITION, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_GUARD_PT, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_PATH, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_IS_USE_GROUP, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_LIGHT_SHADOW, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_OBJPRIMTXTR, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_MIN, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_PARTICL_TYPE, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_MAX_ID, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_MT_SPELL, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_LEVER_SCIENCE_STATS, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_ITEMS, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_CYCLIC, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_HELP, Type = SectionType.ST_FLOAT},
            new MobFileSectionInfo {Id = SectionId.ID_GUARD_PT_POSITION, Type = SectionType.ST_PLOT},
            new MobFileSectionInfo {Id = SectionId.ID_ACTION_PT, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_ID, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_VARIABLE, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_LIGHT_COLOR, Type = SectionType.ST_PLOT},
            new MobFileSectionInfo {Id = SectionId.ID_OBJSECTXTR, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_MAX, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_PARTICL_SCALE, Type = SectionType.ST_FLOAT},
            new MobFileSectionInfo {Id = SectionId.ID_AIGRAPH, Type = SectionType.ST_STR_S08},
            new MobFileSectionInfo {Id = SectionId.ID_MT_AREAS, Type = SectionType.ST_STR_S01},
            new MobFileSectionInfo {Id = SectionId.ID_LEVER_CUR_STATE, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_STATS, Type = SectionType.ST_STR_S06},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_MODEL, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_ALWAYS_ACTIVE, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_GUARD_PT_ACTION, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_ACTION_PT_LOOK_PT, Type = SectionType.ST_PLOT},
            new MobFileSectionInfo {Id = SectionId.ID_TORCH, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_RECT, Type = SectionType.ST_RECT},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_BS_CHECK, Type = SectionType.ST_STR_SAR},
            new MobFileSectionInfo {Id = SectionId.ID_LIGHT_COMMENTS, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_OBJPOSITION, Type = SectionType.ST_PLOT},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_COMMENTS, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_MT_TARGETS, Type = SectionType.ST_STR_S02},
            new MobFileSectionInfo {Id = SectionId.ID_LEVER_TOTAL_STATE, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_QUEST_ITEMS, Type = SectionType.ST_STR_SAR},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_GUARD_R, Type = SectionType.ST_FLOAT},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_AGRESSION_MODE, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_ACTION_PT_WAIT_SEG, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_TORCH_STRENGHT, Type = SectionType.ST_FLOAT},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_SRC_ID, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_SOUND_VOLUME, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_MT_CAST_INTERVAL, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_LEVER_IS_CYCLED, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_QUICK_ITEMS, Type = SectionType.ST_STR_SAR},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_GUARD_PT, Type = SectionType.ST_PLOT},
            new MobFileSectionInfo {Id = SectionId.ID_ACTION_PT_TURN_SPEED, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_TORCH_PTLINK, Type = SectionType.ST_PLOT},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_DST_ID, Type = SectionType.ST_DWORD},
            new MobFileSectionInfo {Id = SectionId.ID_LEVER_CAST_ONCE, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_SPELLS, Type = SectionType.ST_STR_SAR},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_NALARM, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_ACTION_PT_FLAGS, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_TORCH_SOUND, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_TITLE, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_LEVER_SCIENCE_STATS_NEW, Type = SectionType.ST_BINARY},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_WEAPONS, Type = SectionType.ST_STR_SAR},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_USE, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_VSS_COMMANDS, Type = SectionType.ST_STRING},
            new MobFileSectionInfo {Id = SectionId.ID_DIRICTORY_ELEMENTS, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_LEVER_IS_DOOR, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_ARMORS, Type = SectionType.ST_STR_SAR},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_REVENGE, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_DIRICTORY, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_SS_TEXT_OLD, Type = SectionType.ST_SCRIPT},
            new MobFileSectionInfo {Id = SectionId.ID_LEVER_RECALC_GRAPH, Type = SectionType.ST_BYTE},
            new MobFileSectionInfo {Id = SectionId.ID_UNIT_LOGIC_FEAR, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_SC_OBJECTDBFILE, Type = SectionType.ST_NULL},
            new MobFileSectionInfo {Id = SectionId.ID_FOLDER, Type = SectionType.ST_REC},
            new MobFileSectionInfo {Id = SectionId.ID_SS_TEXT, Type = SectionType.ST_SCRIPT_ENC}
        };

        private MobFileSection MainSection;
        public MobFileSection CurrentSection;
        public string Filename;
        public bool Changed;
        
        public MobFile()
        {
            MainSection = new MobFileSection(null);
            Changed = false;
        }
        public MobFile(string filename)
        {
            MainSection = new MobFileSection(null);
            OpenMobFile(filename);
            Changed = false;
        }
        public ErrorCodes OpenMobFile(string filename)
        {
            Filename = filename;
            System.IO.FileStream mob_file;
            try
            {
                mob_file = System.IO.File.Open(filename, System.IO.FileMode.Open);
            }
            catch
            {
                return ErrorCodes.CannotOpen;
            }
            //main_section.data_size = (uint)mob_file.Length;
            MainSection.Data = new byte[(uint)mob_file.Length];
            //main_section.data_offset = 0;
            mob_file.Read(MainSection.Data, 0, MainSection.Data.Length);
            mob_file.Close();

            CurrentSection = MainSection;
            ErrorCodes result = MainSection.ReadSubsections();
            if (result != 0)
            {
                CurrentSection = MainSection = new MobFileSection(null);
            }

            return result;
        }
        public int SaveMobFile(string filename)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
            Stack<MobFileSection> stack = new Stack<MobFileSection>();
            MobFileSection cur = MainSection;
            stack.Push(null);
            do
            {
                if (cur.Items != null)
                {
                    for (int i = cur.Items.Count - 1; i >= 0; --i)
                        stack.Push(cur.Items[i]);
                }

                if (cur.Data != null)
                    fs.Write(cur.Data, 0, cur.Data.Length);

            } while ((cur = stack.Pop()) != null);

            fs.Close();
            Changed = false;
            return 0;
        }
        public void ReturnSection()
        {
            if (CurrentSection.Owner != null)
                CurrentSection = CurrentSection.Owner;
        }
        public int FixSize(int delta)
        {
            MobFileSection cur = CurrentSection;
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
    }
}
