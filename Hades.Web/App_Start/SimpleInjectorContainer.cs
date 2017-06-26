using Hades.Application;
using Hades.Application.Interface;
using SimpleInjector;

namespace Hades.Web
{
    public class SimpleInjectorContainer
    {
        public static Container RegisterContainer()
        {
            var container = new Container();

            container.Register<IUsuarioAppService, UsuarioAppService>();
            container.Register<IEnqueteAppService, EnqueteAppService>();
            container.Register<ISorteioAppService, SorteioAppService>();
            container.Register<ISorteioParticipanteAppService, SorteioParticipanteAppService>();
            container.Register<IVotacaoAppService, VotacaoAppService>();

            container.Verify();
            return container;
        }
    }
}