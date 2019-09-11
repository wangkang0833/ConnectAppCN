using System.Collections.Generic;
using ConnectApp.Constants;
using ConnectApp.Models.Api;
using ConnectApp.Utils;
using Newtonsoft.Json;
using RSG;
using UnityEngine;

namespace ConnectApp.Api {
    public static class ChannelApi {
        public static Promise<FetchPublicChannelsResponse> FetchPublicChannels() {
            var promise = new Promise<FetchPublicChannelsResponse>();
            var para = new Dictionary<string, object> {
                {"k", "[%22q:%22]"},
                {"lt", "public"},
                {"workspaceId", "058d9079fac00000"},
            };
            var request = HttpManager.GET($"{Config.apiAddress}/api/c", parameter: para);
            HttpManager.resume(request).Then(responseText => {
                var articlesResponse = JsonConvert.DeserializeObject<FetchPublicChannelsResponse>(responseText);
                promise.Resolve(articlesResponse);
            }).Catch(exception => { promise.Reject(exception); });
            return promise;
        }
    }
}