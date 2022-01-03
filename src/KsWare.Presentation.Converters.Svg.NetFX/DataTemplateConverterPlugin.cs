using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
using KsWare.Presentation.Interfaces.Plugins.DataTemplateConverter;
using SharpVectors.Converters;

namespace KsWare.Presentation.Converters.Svg
{
	[Export(typeof(IDataTemplateConverterPlugin)), DataTemplateConverterPluginExportMetadata("image/svg+xml")]
	public sealed class DataTemplateConverterPlugin : IDataTemplateConverterPlugin
	{
        public DataTemplate CreateDataTemplate(object content)
        {
	        var locationUri = (Uri) content;
			// REQUIRES: PM> Install-Package SharpVectors
			var dummy = new SvgViewbox(); //WORKAROUND

            var dataTemplateXaml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
			<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:svgc=""http://sharpvectors.codeplex.com/svgc/"" >
				<svgc:SvgViewbox Source=""{locationUri}"" Stretch=""Uniform"" />
			</DataTemplate>";
			
			
            var sr = new StringReader(dataTemplateXaml);
            var xr = XmlReader.Create(sr);
            var dataTemplate = (DataTemplate)XamlReader.Load(xr);
            return dataTemplate;
        }
    }
}
