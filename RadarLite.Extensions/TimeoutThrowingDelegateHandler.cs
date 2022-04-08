namespace RadarLite.Extensions;
public class TimeoutThrowingDelegatingHandler : DelegatingHandler {
    public TimeoutThrowingDelegatingHandler() { }

    public TimeoutThrowingDelegatingHandler(
        HttpMessageHandler innerHandler) : base(innerHandler) { }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            return await base.SendAsync(request, cancellationToken);
        }

        // TODO: this doesn't really
        // innerException is populated by HttpClient after DelegatingHandlers processing
        catch (TaskCanceledException timeoutException)
            when (timeoutException.InnerException is TimeoutException)
        {
            throw timeoutException.InnerException;
        }
    }
}