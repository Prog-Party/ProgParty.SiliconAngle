﻿using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;

namespace ProgParty.Core.Share
{
    public class ShareUrl
    {
        protected string ApplicationLink => GetApplicationLink(GetType().Name);

        public static string GetApplicationLink(string sharePageName) => "ms-sdk-sharesourcecs:navigate?page=" + sharePageName;

        private string _url = null;
        private string _description = null;

        public void RegisterForShare(MenuFlyoutItem menuFlyoutItem, string url, string description = "")
        {
            _url = url;
            _description = description;
            
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += ShareUrlHandler;
            DataTransferManager.ShowShareUI();
        }

        private void ShareUrlHandler(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (_url == null)
                return;

            DataRequest request = args.Request;
            request.Data.Properties.ApplicationName = Config.Instance.AppName;
            request.Data.Properties.Title = $"Share {Config.Instance.AppName} url";
            request.Data.Properties.Description = $"Share {Config.Instance.AppName} url";
            request.Data.Properties.ContentSourceApplicationLink = new Uri(ApplicationLink);
            request.Data.SetDataProvider(StandardDataFormats.WebLink, new DataProviderHandler(this.OnDeferredImageRequestedHandler));
        }

        private void OnDeferredImageRequestedHandler(DataProviderRequest request)
        {
            if (_url != null)
            {
                // If the delegate is calling any asynchronous operations it needs to acquire
                // the deferral first. This lets the system know that you are performing some
                // operations that might take a little longer and that the call to SetData 
                // could happen after the delegate returns. Once you acquired the deferral object 
                // you must call Complete on it after your final call to SetData.
                DataProviderDeferral deferral = request.GetDeferral();

                // Make sure to always call Complete when finished with the deferral.
                try
                {
                    request.SetData(_url);
                }
                finally
                {
                    deferral.Complete();
                }
            }
        }
    }
}
