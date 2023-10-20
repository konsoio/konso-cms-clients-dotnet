using GetCms.Models.Cms.Enums;
using GetCms.Models.Menus.Dtos;
using GetCms.Models.Pages.Dtos;
using GetInfra.Standard.Caching;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using Yasmin.yaIdentity.Web.Config;
using Yasmin.yaIdentity.Web.Models;
using Yasmin.yaIdentity.Web.Models.Sites;

namespace Yasmin.yaIdentity.Web.Services
{
    public class CacheLoadService : IHostedService
    {
        private Timer _timer;
        private readonly ICache _cache;
        private readonly IKonsoSitesClient _sitesClient;
        private readonly IKonsoPagesClient _pagesClient;
        private readonly IKonsoContentsClient _contentsClient;
        private readonly IKonsoMenusClient _menusClient;
        private readonly IKonsoSiteService _siteService;
        private readonly KonsoCmsConfig _config;
        public CacheLoadService(ICache cache, 
            IKonsoSitesClient sitesClient, 
            IKonsoPagesClient pagesClient, 
            IKonsoContentsClient contentsClient,
            IKonsoMenusClient menusClient,
            IOptions<KonsoCmsConfig> config,
            IKonsoSiteService siteService)
        {
            _cache = cache;
            _sitesClient = sitesClient;
            _pagesClient = pagesClient;
            _contentsClient = contentsClient;
            _menusClient = menusClient;
            _config = config.Value;
            _siteService = siteService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //await LoadSites();

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
              TimeSpan.FromMinutes(10));
        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }


        private void DoWork(object state)
        {
            foreach (var site in _config.Sites)
            {
                LoadSites(site).ConfigureAwait(false);
                LoadPages(site).ConfigureAwait(false);
                LoadMenus(site).ConfigureAwait(false);
            }
        }


        private async Task LoadSites(KonsoCmsSite siteConfig)
        {

            var sites = await _sitesClient.GetByBucketIdAsync(siteConfig);
            foreach (var site in sites)
            {
                _siteService.SetSiteToCache(site, siteConfig);
            }
        }

        private async Task LoadMenus(KonsoCmsSite siteConfig)
        {

            var menus = await _menusClient.GetByBucketIdAsync(siteConfig, 0, 10);
            foreach (var m in menus.List)
            {
                _cache.UpdateInHash<MenuDto<int>>(string.Format(CacheKeys.MenusByBucket, siteConfig.BucketId), m.Name, m);
            }
        }


        private async Task LoadPages(KonsoCmsSite siteConfig)
        {

            int count = 0;
            long total = 0;

            while (count < total || count == 0)
            {
                var pagesRes = await _pagesClient.GetByBucketIdAsync(siteConfig, null, null, null, count + 1, count + 10);

                if (total == 0)
                    total = pagesRes.Total;

                foreach (var page in pagesRes.List)
                {
                    if(!string.IsNullOrEmpty(page.Slug))
                        _cache.UpdateInHash<PageDto<int>>(string.Format(CacheKeys.PagesBySlug, siteConfig.BucketId), page.Slug, page);
                    _cache.UpdateInHash<PageDto<int>>(string.Format(CacheKeys.PagesById, siteConfig.BucketId), page.Id.ToString(), page);

                    if(page.PageType == PageTypes.MasterPage && page.IsSystem)
                        _cache.Add(string.Format(CacheKeys.DefaultMasterPageByBucket, siteConfig.BucketId), page.Id.ToString());
                    count++;
                }
            }
        }

        private async Task LoadContent(KonsoCmsSite siteConfig)
        {

            int count = 0;
            long total = 0;

            while (count < total || count == 0)
            {
                var pagesRes = await _pagesClient.GetByBucketIdAsync(siteConfig, null, null, null, count + 1, count + 10);

                if (total == 0)
                    total = pagesRes.Total;

                foreach (var page in pagesRes.List)
                {
                    if (!string.IsNullOrEmpty(page.Slug))
                        _cache.UpdateInHash<PageDto<int>>(string.Format(CacheKeys.PagesBySlug, page.SiteId), page.Slug, page);
                    _cache.UpdateInHash<PageDto<int>>(string.Format(CacheKeys.PagesById, page.SiteId), page.Id.ToString(), page);
                    count++;
                }
            }
        }
    }
}
