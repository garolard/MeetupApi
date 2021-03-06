﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Meetup.Api.Annotations;

// ReSharper disable once CheckNamespace
namespace Meetup.Api
{
    public class MeetupEvent
    {
        /// <summary>
        /// Fetches a Meetup Event by group urlname and event_id
        /// </summary>
        /// <param name="urlName">The urlname path element may be any valid group urlname.</param>
        /// <param name="id">The id path element must be a valid alphanumeric Meetup Event identifier</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task&lt;Event&gt;.</returns>
        /// <exception cref="HttpRequestException">
        ///     Ops! Something went wrong :S. Please try again, if the error persist contact
        ///     with the developer to fix the issue.
        /// </exception>
        public async Task<Event> ByIdAsync([NotNull]string urlName, [NotNull]string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(urlName)) throw new ArgumentException("Argument is null or empty", nameof(urlName));
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Argument is null or empty", nameof(id));
            if (string.IsNullOrWhiteSpace(urlName)) throw new ArgumentException("Argument is null or whitespace", nameof(urlName));
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Argument is null or whitespace", nameof(id));

            var queryUrl = new StringBuilder(MeetupBase.BASE_URL);
            queryUrl.Append($"/{urlName}/events/{id}/");

            var response =
                await MeetupBase.ExecuteQueryAsync<Event>(queryUrl, cancellationToken);

            if (response == null)
                throw new HttpRequestException(
                    "Ops! Something went wrong :S. Please try again, if the error persist contact with the developer to fix the issue.");
            return response;
        }
    }
}