namespace TachGame {
    public enum MessagesCode {
        UPDATE_MAP = 8002,
        /*
         * Arg1: 
         * Arg2: 
         * Arg3: 
         * Obj1: 
         * Obj2:
         * Obj3:
         */
        READ_MAP_FROM_CSV = 1002,
        /*
         * Arg1: Row
         * Arg2: Column
         * Arg3: 
         * Obj1: TileTypes[] Map
         * Obj2:
         * Obj3:
         */

        READ_MAP_FROM_SAVEDATA = 8001,
        /*
         * Arg1: 0/1
         * Arg2: 
         * Arg3: 
         * Obj1: TileTypes[] Map
         * Obj2: string path
         * Obj3:
         */

        RENDER_NEW_MAP = 9001,
        /*
         * Arg1: 
         * Arg2: 
         * Arg3: 
         * Obj1: MapSaveData[] Map
         * Obj2: 
         * Obj3:
         */
        UPDATE_AC_MAP = 7001,
        /*
         * Arg1: Row
         * Arg2: Column
         * Arg3: 
         * Obj1: int[] ACMap
         * Obj2:
         * Obj3:
         */
        READ_CHARA_FROM_SAVEDATA = 5001,
        /*
         * Arg1: 
         * Arg2: 
         * Arg3: 
         * Obj1: string path
         * Obj2:
         * Obj3:
         */
        RENDER_NEW_CHARACTER = 6001
        /*
         * Arg1: 
         * Arg2: 
         * Arg3: 
         * Obj1: string path
         * Obj2:
         * Obj3:
         */
    }

    public enum ManagerCode {
        MAP_RENDERER = 9000,
        READER = 1000,
        WRITER = 2000,
        CHARA = 5000,
        CHARA_RENDERER = 6000,

        MAP_MANAGER = 8000,
        NAVI = 7000
    }
}
