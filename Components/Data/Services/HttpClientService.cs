﻿using System;
using System.Net.Http;

namespace AttendanceManagement.Services
{
    /// <summary>
    /// Service that provides
    /// HttpClient instance
    /// </summary>
    public class HttpClientService
    {
        public HttpClient HttpClient { get; private set; }

        private static object _httpClientHandler = null;
        public static object HttpClientHandler
        {
            get => _httpClientHandler;
            set
            {
                if (_httpClientHandler == null)
                {
                    if (value is HttpMessageHandler)
                        _httpClientHandler = value;
                    else
                        throw new Exception($"{nameof(HttpClientHandler)} incorrect type!");
                }
                else
                    throw new Exception($"{nameof(HttpClientHandler)} is already setup!");
            }
        }

        private static HttpClientService _instance = null;
        private static readonly object Padlock = new object();

        public HttpClientService()
        {
            HttpClient = HttpClientHandler != null ?
                new HttpClient((HttpMessageHandler)HttpClientHandler) : new HttpClient();
        }

        public static HttpClientService Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ?? (_instance = new HttpClientService());
                }
            }
        }
    }
}