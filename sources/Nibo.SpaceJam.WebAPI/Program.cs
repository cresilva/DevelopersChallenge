using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Nibo.SpaceJam.WebAPI
{
    /// <summary>
    /// Main class of application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point of application
        /// </summary>
        /// <param name="args">Arguments of initialization</param>
        public static void Main(string[] args) => BuildWebHost(args).Run();

        /// <summary>
        /// Build host of application
        /// </summary>
        /// <param name="args">Arguments of initialization</param>
        /// <returns>Instance of webhost</returns>
        public static IWebHost BuildWebHost(string[] args) =>  WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();
    }
}
