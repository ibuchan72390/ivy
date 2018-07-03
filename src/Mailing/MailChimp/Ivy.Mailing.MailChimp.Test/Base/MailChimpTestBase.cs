using Ivy.TestHelper;
using Ivy.Web.IoC;
using Ivy.Mailing.MailChimp.IoC;
using Microsoft.Extensions.DependencyInjection;
using Ivy.IoC.Core;

namespace Ivy.Mailing.MailChimp.Test.Base
{
    public class MailChimpTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyWeb();
            containerGen.InstallIvyMailChimp();

            var svcColl = containerGen.GetServiceCollection();

            svcColl.AddLogging();
        }
    }
}
