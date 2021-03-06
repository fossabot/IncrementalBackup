﻿using RocksDbSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IncrementalBackup {
    class Program {
        static string Help = @"
备份：
IB backup d:/from d:/to
两个都应该是绝对路径，且最后无'/'， 第一个是待备份文件目录，第二个是备份至文件目录

回滚：
IB rollback d:/from d:/to
两个都应该是绝对路径，且最后无'/'， 第一个是上文备份至文件目录，第二个是回滚至文件目录
";

        static async Task Main(string[] args) {
            var Before = DateTime.Now;
            if (args.Length != 3) {
                Console.WriteLine(Help);
            } else {
                switch (args[0].ToLower()) {
                    case "backup":
                        await new Startup($"{Utils.ConvertPath(args[2])}/Data").Backup(Utils.ConvertPath(args[1]), Utils.ConvertPath(args[2]));
                        var After = DateTime.Now;
                        Console.WriteLine($"备份完成！\n共耗时: {After - Before}");
                        break;
                    case "rollback":
                        await new Startup($"{Utils.ConvertPath(args[1])}/Data").Rollback(Utils.ConvertPath(args[2]));
                        After = DateTime.Now;
                        Console.WriteLine($"回滚完成！\n共耗时: {After - Before}");
                        break;
                    default:
                        break;
                }
            }
            

        }
    }
}
