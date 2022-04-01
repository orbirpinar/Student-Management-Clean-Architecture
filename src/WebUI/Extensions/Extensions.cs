using System;
using System.ComponentModel;
using Application.Common.Converters;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Extensions
{
    public static class Extensions
    {
        public static MvcOptions UseDateOnlyStringConverters(this MvcOptions mvcOptions)
        {
            TypeDescriptor.AddAttributes(typeof(DateOnly), new TypeConverterAttribute(typeof(DateOnlyTypeConverter)));
            return mvcOptions;
        }
    }
}