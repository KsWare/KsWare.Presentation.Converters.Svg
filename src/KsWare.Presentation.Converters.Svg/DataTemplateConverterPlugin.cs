using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
using KsWare.Presentation.Interfaces.Plugins.ResourceConverter;
using SharpVectors.Converters;

namespace KsWare.Presentation.Converters.Svg {

	[Export(typeof(ResourceConverterPlugin)), ResourceConverterPluginExportMetadata("image/svg+xml")]
	public sealed class ResourceConverterPlugin : IResourceConverterPlugin {

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (typeof(DataTemplate).IsAssignableFrom(targetType)) return CreateDataTemplate(value);
			throw new NotImplementedException("Conversion not supported");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}

		private object CreateDataTemplate(object content) {
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
