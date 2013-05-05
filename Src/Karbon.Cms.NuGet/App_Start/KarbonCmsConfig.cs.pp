[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.KarbonCmsConfig), "Start")]
namespace $rootnamespace$.App_Start
{
    public class KarbonCmsConfig
    {
        public static void Start()
        {
            new Karbon.Cms.Web.WebBootManager().Initialize();
        }
    }
}