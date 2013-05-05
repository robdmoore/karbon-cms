using Karbon.Cms.Web;

[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.KarbonConfig), "Init")]
namespace $rootnamespace$.App_Start
{
    public class KarbonConfig
    {
        public static void Init()
        {
            new WebBootManager()
                .Initialize();
        }
    }
}