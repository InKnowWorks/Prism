using CommonServiceLocator;
using DryIoc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.DryIoc.Regions
{
    /// <summary>
    /// 
    /// </summary>
    public class DryIocRegionNavigationContentLoader : RegionNavigationContentLoader
    {
        private readonly IContainer _container;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceLocator"></param>
        /// <param name="container"></param>
        public DryIocRegionNavigationContentLoader(IServiceLocator serviceLocator, IContainer container) : base(serviceLocator)
        {
            _container = container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        /// <param name="candidateNavigationContract"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected override IEnumerable<object> GetCandidatesFromRegion(IRegion region, string candidateNavigationContract)
        {
            if (candidateNavigationContract == null || candidateNavigationContract.Equals(string.Empty))
                throw new ArgumentNullException(nameof(candidateNavigationContract));

            IEnumerable<object> contractCandidates = base.GetCandidatesFromRegion(region, candidateNavigationContract);

            var candidatesFromRegion = contractCandidates as object[] ?? contractCandidates.ToArray();

            if (!candidatesFromRegion.Any())
            {
                var matchingRegistration = _container.GetServiceRegistrations().FirstOrDefault(r => candidateNavigationContract.Equals(r.OptionalServiceKey?.ToString(), StringComparison.Ordinal));
                if (matchingRegistration.OptionalServiceKey == null)
                    matchingRegistration = _container.GetServiceRegistrations().FirstOrDefault(r => candidateNavigationContract.Equals(r.ImplementationType.Name, StringComparison.Ordinal));

                if (matchingRegistration.ServiceType == null) 
                    return new object[0];

                string typeCandidateName = matchingRegistration.ImplementationType.FullName;
                contractCandidates = base.GetCandidatesFromRegion(region, typeCandidateName);
            }

            return candidatesFromRegion;
        }
    }
}
