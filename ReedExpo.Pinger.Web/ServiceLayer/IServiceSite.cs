using Domain;
using System.Collections.Generic;

namespace ServiceLayer
{
    public interface IServiceSite
    {
        void CreateSite(Site site);
        void DeleteSite(Site site);
        void EditSite(Site siteModel, int id);
        Site GetSite(long id);
        IList<SiteDto> GetAllSites();
        IList<FullSiteDto> GetAllFullSites();
    }
}