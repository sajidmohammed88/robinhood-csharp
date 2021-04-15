﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RobinhoodLibrary.Abstractions
{
    /// <summary>
    /// HTTP client manager interface.
    /// </summary>
    public interface IHttpClientManager : IDisposable
    {
        /// <summary>
        /// Get data asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="autoLog">The auto log.</param>
        /// <param name="query">Dictionary that represent the query string.</param>
        /// <returns>The result of get request.</returns>
        Task<T> GetAsync<T>(string url, bool autoLog = true, IDictionary<string, string> query = null);

        /// <summary>
        /// Post data asynchronously.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <param name="autoLog">if set to <c>true</c> [automatic log].</param>
        /// <returns>Tuple of the status code and deserialized data.</returns>
        Task<(HttpStatusCode StatusCode, T Data)> PostAsync<T>(string url, IDictionary<string, string> data,
            bool autoLog = true) where T : class;

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <param name="specifiedHeader">The specified header.</param>
        /// <returns>The status code</returns>
        Task<(HttpStatusCode StatusCode, string Result)> PostAsync(string url,
            IDictionary<string, string> data, (string Name, string Value) specifiedHeader);
    }
}