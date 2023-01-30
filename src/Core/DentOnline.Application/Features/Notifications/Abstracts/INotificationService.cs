namespace DentOnline.Application.Features.Notifications.Abstracts;

public interface INotificationService
{
    public Task<IResponse> SendNotificationWhenCreatedATreatmentAsync(string treatmentId);
    public Task<IResponse> SendNotificationWhenAddedACommentToTreatmentAsync(string treatmentId);
    public Task<IResponse> SendEmailActivationNotificationToDoctorAsync(string userId);
}