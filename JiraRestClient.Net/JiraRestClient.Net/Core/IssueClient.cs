using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Cschulc.Jira.Util;
using JiraRestClient.Net.Domain.Issue;
using JiraRestClient.Net.Util;

namespace JiraRestClient.Net.Core
{
    /// <summary>
    /// Client to get Issues
    /// </summary>
    public class IssueClient : BaseClient
    {
        public IssueClient(JiraRestClient jiraRestClient) : base(jiraRestClient)
        {
        }

        /// <summary>
        /// Get a Issue by key
        /// </summary>
        /// <param name="key">The key of the Issue</param>
        /// <returns>A async Task containing the Issue</returns>
        public Issue GetIssueByKey(string key)
        {
            var restUriBuilder = UriHelper.BuildPath(BaseUri, RestPathConstants.Issue, key);
            var completeUri = restUriBuilder.ToString();
            var stream = Client.GetStringAsync(completeUri);
            var streamResult = stream.Result;
            return JsonSerializer.Deserialize<Issue>(streamResult);
        }


        public Issue GetIssueByKey(string key, List<string> fields, List<string> expand)
        {
            var restUriBuilder = UriHelper.BuildPath(BaseUri, RestPathConstants.Issue, key);
            if (fields != null && fields.Count > 0)
            {
                var fieldsParam = string.Join(",", fields);
                UriHelper.AddQuery(restUriBuilder, RestParamConstants.Fields, fieldsParam);
            }

            if (expand != null && expand.Count > 0)
            {
                var expandParam = string.Join(",", expand);
                UriHelper.AddQuery(restUriBuilder, RestParamConstants.Expand, expandParam);
            }

            var completeUri = restUriBuilder.ToString();
            var stream = Client.GetStringAsync(completeUri);
            var streamResult = stream.Result;
            return JsonSerializer.Deserialize<Issue>(streamResult);
        }

        public IssueResponse CreateIssue(IssueUpdate issue)
        {
            var json = JsonSerializer.Serialize(issue, new JsonSerializerOptions
            {
                IgnoreNullValues = true
            });
            var uri = UriHelper.BuildPath(BaseUri, RestPathConstants.Issue);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = Client.PostAsync(uri.ToString(), httpContent);
            var httpResponseMessage = response.Result;
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                {
                    var readAsStringAsync = httpResponseMessage.Content.ReadAsStringAsync();
                    var result = readAsStringAsync.Result;
                    var createdIssue = JsonSerializer.Deserialize<Issue>(result);
                    return new IssueResponse
                    {
                        Key = createdIssue.Key
                    };
                }
                case HttpStatusCode.BadRequest:
                {
                    var readAsStringAsync = httpResponseMessage.Content.ReadAsStringAsync();
                    var result = readAsStringAsync.Result;
                    return JsonSerializer.Deserialize<IssueResponse>(result);
                }
                case HttpStatusCode.Accepted:
                    break;
                case HttpStatusCode.Ambiguous:
                    break;
                case HttpStatusCode.BadGateway:
                    break;
                case HttpStatusCode.Conflict:
                    break;
                case HttpStatusCode.Continue:
                    break;
                case HttpStatusCode.Created:
                {
                    var readAsStringAsync = httpResponseMessage.Content.ReadAsStringAsync();
                    var result = readAsStringAsync.Result;
                    var createdIssue = JsonSerializer.Deserialize<Issue>(result);
                    return new IssueResponse
                    {
                        Key = createdIssue.Key
                    };
                }
                case HttpStatusCode.ExpectationFailed:
                    break;
                case HttpStatusCode.Forbidden:
                    break;
                case HttpStatusCode.Found:
                    break;
                case HttpStatusCode.GatewayTimeout:
                    break;
                case HttpStatusCode.Gone:
                    break;
                case HttpStatusCode.HttpVersionNotSupported:
                    break;
                case HttpStatusCode.InternalServerError:
                {
                    var readAsStringAsync = httpResponseMessage.Content.ReadAsStringAsync();
                    var result = readAsStringAsync.Result;
                    return JsonSerializer.Deserialize<IssueResponse>(result);
                }
                case HttpStatusCode.LengthRequired:
                    break;
                case HttpStatusCode.MethodNotAllowed:
                    break;
                case HttpStatusCode.Moved:
                    break;
                case HttpStatusCode.NoContent:
                    break;
                case HttpStatusCode.NonAuthoritativeInformation:
                    break;
                case HttpStatusCode.NotAcceptable:
                    break;
                case HttpStatusCode.NotFound:
                    break;
                case HttpStatusCode.NotImplemented:
                    break;
                case HttpStatusCode.NotModified:
                    break;
                case HttpStatusCode.PartialContent:
                    break;
                case HttpStatusCode.PaymentRequired:
                    break;
                case HttpStatusCode.PreconditionFailed:
                    break;
                case HttpStatusCode.ProxyAuthenticationRequired:
                    break;
                case HttpStatusCode.RedirectKeepVerb:
                    break;
                case HttpStatusCode.RedirectMethod:
                    break;
                case HttpStatusCode.RequestedRangeNotSatisfiable:
                    break;
                case HttpStatusCode.RequestEntityTooLarge:
                    break;
                case HttpStatusCode.RequestTimeout:
                    break;
                case HttpStatusCode.RequestUriTooLong:
                    break;
                case HttpStatusCode.ResetContent:
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    break;
                case HttpStatusCode.SwitchingProtocols:
                    break;
                case HttpStatusCode.Unauthorized:
                    break;
                case HttpStatusCode.UnsupportedMediaType:
                    break;
                case HttpStatusCode.Unused:
                    break;
                case HttpStatusCode.UpgradeRequired:
                    break;
                case HttpStatusCode.UseProxy:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }
    }
}