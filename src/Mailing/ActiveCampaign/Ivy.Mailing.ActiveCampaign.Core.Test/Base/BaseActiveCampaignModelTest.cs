using Ivy.IoC;
using Ivy.Web.Core.Json;
using Ivy.Web.IoC;

namespace Ivy.Mailing.ActiveCampaign.Core.Test.Base
{
    public class BaseActiveCampaignModelTest<T>
    {
        protected IJsonSerializationService _serializer;
        protected IJsonManipulationService _jsonManipulator;

        public BaseActiveCampaignModelTest()
        {
            var containerGen = new ContainerGenerator();

            containerGen.InstallIvyWeb();

            var container = containerGen.GenerateContainer();

            _serializer = container.GetService<IJsonSerializationService>();
            _jsonManipulator = container.GetService<IJsonManipulationService>();
        }

        protected T TestJsonConvert(string json)
        {
            return _serializer.Deserialize<T>(json);
        }
    }
}
