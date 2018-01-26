using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Services;
using Ivy.Mailing.ActiveCampaign.Core.Models;
using Ivy.Web.Core.Json;
using System.Collections.Generic;

namespace Ivy.Mailing.ActiveCampaign.Services
{
    public class ActiveCampaignContactListDeserializer : IActiveCampaignContactListDeserializer
    {
        #region Variables & Constnats

        private readonly IJsonManipulationService _jsonManipulator;
        private readonly IJsonSerializationService _jsonSerializer;

        const string codeAttr = "result_code";
        const string msgAttr = "result_message";
        const string outAttr = "result_output";

        private readonly IList<string> invalidAttrs = new List<string>
        {
            codeAttr, msgAttr, outAttr
        };

        #endregion

        #region Constructor

        public ActiveCampaignContactListDeserializer(
            IJsonManipulationService jsonManipulator,
            IJsonSerializationService jsonSerializer)
        {
            _jsonManipulator = jsonManipulator;
            _jsonSerializer = jsonSerializer;
        }

        #endregion

        #region Public Methods

        public ActiveCampaignContactList Deserialize(string json)
        {
            // We're going to have 3 invalid JSON attributes prior to cast
            var resultCode = _jsonManipulator.ExtractJsonAttribute<int>(json, codeAttr);
            var resultMessage = _jsonManipulator.ExtractJsonAttribute<string>(json, msgAttr);
            var resultOut = _jsonManipulator.ExtractJsonAttribute<string>(json, outAttr);

            foreach (var attr in invalidAttrs)
            {
                json = _jsonManipulator.RemoveJsonAttribute(json, attr);
            }

            var result = _jsonSerializer.Deserialize<ActiveCampaignContactList>(json);

            result.result_code = resultCode;
            result.result_message = resultMessage;
            result.result_output = resultOut;

            return result;
        }

        #endregion
    }
}
