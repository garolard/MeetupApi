﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.String;

// ReSharper disable once CheckNamespace
namespace Meetup.Api
{
    public class MeetupEvent
    {
        /// <summary>
        ///     Fetches a Meetup Event by group urlname and event_id
        /// </summary>
        /// <param name="urlName">The urlname path element may be any valid group urlname.</param>
        /// <param name="id">The id path element must be a valid alphanumeric Meetup Event identifier</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// <exception cref="HttpRequestException">
        ///     Ops! Something went wrong :S. Please try again, if the error persist contact
        ///     with the developer to fix the issue.
        /// </exception>
        public async Task<Event> ByIdAsync(string urlName, string id, CancellationToken cancellationToken)
        {
            if (IsNullOrEmpty(urlName)) throw new ArgumentException("Argument is null or empty", nameof(urlName));
            if (IsNullOrEmpty(id)) throw new ArgumentException("Argument is null or empty", nameof(id));

            var queryUrl = new StringBuilder(MeetupBase.BASE_URL);
            queryUrl.Append($"/{urlName}/events/{id}/");

            var response =
                await MeetupBase.ExecuteQueryAsync<Event>(queryUrl, cancellationToken, null, HttpMethodTypes.GET);

            if (response == null)
                throw new HttpRequestException(
                    "Ops! Something went wrong :S. Please try again, if the error persist contact with the developer to fix the issue.");
            return response;
        }

        /// <summary>
        ///     Get all attendance of an specific event
        /// </summary>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// <exception cref="HttpRequestException">
        ///     Ops! Something went wrong :S. Please try again, if the error persist contact
        ///     with the developer to fix the issue.
        /// </exception>
        public async Task<Event> AttendanceAsync(string urlName, string id, CancellationToken cancellationToken)
        {
            var queryUrl = new StringBuilder(MeetupBase.BASE_URL);
            queryUrl.Append($"/{urlName}/events/{id}/attendance");

            var response =
                await MeetupBase.ExecuteQueryAsync<Event>(queryUrl, cancellationToken, null, HttpMethodTypes.GET);

            if (response == null)
                throw new HttpRequestException(
                    "Ops! Something went wrong :S. Please try again, if the error persist contact with the developer to fix the issue.");
            return response;
        }

        ///// Post all attendance of an specific event

        ///// <summary>
        ///// </summary>
        ///// <returns>Task&lt;System.Boolean&gt;.</returns>
        ///// <exception cref="HttpRequestException">Ops! Something went wrong :S. Please try again, if the error persist contact with the developer to fix the issue.</exception>
        //internal static async Task<bool> PostEventAttendanceAsync(string urlName, string id, CancellationTokenSource cancellationTokenSource)
        //{
        //    var queryUrl = new StringBuilder(MeetupBase.BASE_URL);
        //    queryUrl.Append($"/{urlName}/events/{id}/attendance");

        //    //var content = new StringContent(message, Encoding.UTF8, "application/json");

        //    var response = await MeetupBase.ExecuteQueryAsync<object>(queryUrl, null, HttpMethodTypes.GET, cancellationTokenSource);

        //    if (response == null)
        //        throw new HttpRequestException("Ops! Something went wrong :S. Please try again, if the error persist contact with the developer to fix the issue.");
        //    return true;
        //}
    }
}