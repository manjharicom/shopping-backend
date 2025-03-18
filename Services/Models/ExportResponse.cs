namespace Services.Models
{
	public class ExportResponse
	{
		public string VirtualPath { get; set; }

		public ExportResponse(string virtualPath)
		{
			VirtualPath = virtualPath;
		}
	}
}
