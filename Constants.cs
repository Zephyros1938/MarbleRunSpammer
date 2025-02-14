using System;
using System.Collections.Generic;

namespace Zephyros1938.Marblerun;
public static class RequestData
{
    public static class Headers
    {
        public const string Accept = "application/json"; // "Accept"
        public const string AcceptEncoding = "gzip, deflate, br, zstd"; // "Accept-Encoding"
        public const string AcceptLanguage = "en-US,en;q=0.5"; // "Accept-Language"
        public const string Connection = "keep-alive"; // "Connection"
        public const string ContentLength = "3381"; // "Content-Length"
        public const string ContentType = "application/x-www-form-urlencoded; charset=UTF-8"; // "Content-Type"
        public const string SessionCookie = "_marblerunv2_session=BAh7B..."; // "Cookie"
        public const string Host = "www.marblerun.at"; // "Host"
        public const string Origin = "https://www.marblerun.at"; // "Origin"
        public const string Priority = "u=0"; // "Priority"
        public const string Referer = "https://www.marblerun.at/"; // "Referer"
        public const string SecFetchDest = "empty"; // "Sec-Fetch-Dest"
        public const string SecFetchMode = "cors"; // "Sec-Fetch-Mode"
        public const string SecFetchSite = "same-origin"; // "Sec-Fetch-Site"
        public const string UserAgent = "Mozilla/5.0 (X11; Linux x86_64; rv:135.0) Gecko/20100101 Firefox/135.0"; // "User-Agent"
        public const string XPrototypeVersion = "1.7_rc2"; // "X-Prototype-Version"
        public const string XRequestedWith = "XMLHttpRequest"; // "X-Requested-With"

        public static readonly Dictionary<string, string> RequestHeaders = new()
        {
            { "Accept", "application/json" },
            { "Accept-Encoding", "gzip, deflate, br, zstd" },
            { "Accept-Language", "en-US,en;q=0.5" },
            { "Connection", "keep-alive" },
            //{ "Content-Length", "3381" },
            //{ "Content-Type", "application/x-www-form-urlencoded; charset=UTF-8" },
            //{ "Cookie", "_marblerunv2_session=BAh7B..." },
            { "Host", "www.marblerun.at" },
            { "Origin", "https://www.marblerun.at" },
            { "Priority", "u=0" },
            { "Referer", "https://www.marblerun.at/" },
            { "Sec-Fetch-Dest", "empty" },
            { "Sec-Fetch-Mode", "cors" },
            { "Sec-Fetch-Site", "same-origin" },
            { "User-Agent", "Mozilla/5.0 (X11; Linux x86_64; rv:135.0) Gecko/20100101 Firefox/135.0" },
            { "X-Prototype-Version", "1.7_rc2" },
            { "X-Requested-With", "XMLHttpRequest" }
        };
    }

    public static class TrackData
    {
        public static readonly Dictionary<int, (string Type, int Rotation, int Row, int Col)> Bricks = new()
        {
            { 0, ("Ball", 0, 0, 0) },
            { 28, ("Brick", 0, 2, 8) },
            { 140, ("Exit", 0, 14, 0) }
        };

        public const double Length = 1.3679063999999992;
        public const int Duration = 716;
        public const string ImageData = "data:image/png;base64,iVBORw0KGgoAAAANSUhE...";
        public const string Username = "test";
        public const string TrackName = "test";
    }
}
