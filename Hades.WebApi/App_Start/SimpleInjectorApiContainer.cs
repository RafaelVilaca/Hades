using Hades.Domain.Interfaces.Repositories;
using Hades.Domain.Interfaces.Services;
using Hades.Domain.Services;
using Hades.Infra.Data.Repositories;
using SimpleInjector;

namespace Hades.WebApi
{
    public static class SimpleInjectorApiContainer
    {
        public static Container RegisterContainer()
        {
            var container = new Container();            

            container.Register<IEnqueteService, EnqueteService>();
            container.Register<IEnqueteRepository, EnqueteRepository>();

            container.Register<ISorteioService, SorteioService>();
            container.Register<ISorteioRepository, SorteioRepository>();

            container.Register<ISorteioParticipanteRepository, SorteioParticipanteRepository>();
            container.Register<ISorteioParticipanteService, SorteioParticipanteService>();

            container.Register<IUsuarioService, UsuarioService>();
            container.Register<IUsuarioRepository, UsuarioRepository>();

            container.Register<IVotacaoRepository, VotacaoRepository>();
            container.Register<IVotacaoService, VotacaoService>();

            container.Register<ICampanhaService, CampanhaService>();
            container.Register<ICampanhaRepository, CampanhaRepository>();

            container.Register<ICampanhaParticipanteService, CampanhaParticipanteService>();
            container.Register<ICampanhaParticipanteRepository, CampanhaParticipanteRepository>();

            container.Verify();
            return container;
        }
    }
}