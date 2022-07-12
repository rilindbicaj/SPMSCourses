using Application.Events;

namespace Application.EventPublisher
{
    public interface IEventPublisher
    {
        void PublishStudentGradedEvent(StudentGradedEvent eventData);
    }
}