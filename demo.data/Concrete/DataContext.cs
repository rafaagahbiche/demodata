namespace demo.data
{
	using System.Web;
	using System.Xml.Linq;
    using Kaliko;
    using System;

	public class DataContext: IDataContext
	{
		private XDocument _dataXml;

		private string originalPath;
		private string userPath;

		public DataContext(string originalDataPath, string userDataPath)
		{
            try
            {
                this.originalPath = HttpContext.Current.Request.MapPath(originalDataPath);
                this.userPath = HttpContext.Current.Request.MapPath(userDataPath);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, Logger.Severity.Critical);
            }

		}

		public DataContext(string originalDataPath)
		{
            try
            {
                this.userPath = HttpContext.Current.Request.MapPath(originalDataPath);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, Logger.Severity.Critical);
            }
		}

		public void SaveFile()
		{
            try
            {
                _dataXml.Save(userPath);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, Logger.Severity.Critical);
            }
        }

		private void CopyData()
		{
            try
            {
                _dataXml = XDocument.Load(originalPath);
                var newCopy = new XDocument(_dataXml);
                newCopy.Save(userPath);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, Logger.Severity.Critical);
            }
		}

		public XDocument DataXml
		{
			get
			{
				if (_dataXml == null)
				{
                    var newSession = HttpContext.Current.Session.IsNewSession;
					if (newSession && !string.IsNullOrEmpty(originalPath))
					{
                        Logger.Write(string.Format("New session the {0} at :{1}", 
                            System.DateTime.Today, 
                            System.DateTime.Now), Logger.Severity.Info);
                        CopyData();
					}

                    try
                    {
                        _dataXml = XDocument.Load(userPath);
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex, Logger.Severity.Critical);
                    }

				}

				return _dataXml;
			}
		}
	}
}
