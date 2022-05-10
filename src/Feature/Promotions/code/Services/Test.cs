using Foundation.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn.Feature.Promotions.Services
{
    public interface ITest
    {

    }

    [Service(Lifetime = Lifetime.Transient, ServiceType = typeof(ITest))]
    public class Test
    {

    }
}