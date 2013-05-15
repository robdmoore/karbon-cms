using Karbon.Cms.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.KarbonCmsConfig), "AppStarting")]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof($rootnamespace$.App_Start.KarbonCmsConfig), "AppStarted")]
namespace $rootnamespace$.App_Start
{
    public class KarbonCmsConfig
    {
        public static void AppStarting()
        {
            new WebBootManager().AppStarting();
        }

        public static void AppStarted()
        {
            new WebBootManager().AppStarted();
        }
    }
}