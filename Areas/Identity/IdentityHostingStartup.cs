﻿using System;
using LibraryCS.Areas.Identity.Data;
using LibraryCS.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(LibraryCS.Areas.Identity.IdentityHostingStartup))]
namespace LibraryCS.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {

               // services.AddDefaultIdentity<LibraryUser>()
               //     .AddDefaultUI(UIFramework.Bootstrap4)
               //     .AddEntityFrameworkStores<LibraryAuthContext>();
            });
        }
    }
}