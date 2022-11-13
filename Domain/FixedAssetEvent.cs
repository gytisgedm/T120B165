namespace api.Domain
{
    public enum FixedAssetEventType
    {
        Unassigned,
        AssignedToUserByManager,
        AcceptedByUser,
        RejectedByUser,
        ReturnInitiatedByManager,
        StoredByManager
    }

    public class FixedAssetEvent
    {
        public int Id { get; set; }
        public FixedAssetEventType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public string? AssignedTo { get; set; }
        public string FixedAssetCode { get; set; }

        public FixedAssetEvent()
        {
            CreatedAt = DateTime.Now;
        }
    }

    public class AssignedToUserEvent : FixedAssetEvent
    {
        public AssignedToUserEvent()
        {
            Type = FixedAssetEventType.AssignedToUserByManager;
        }
    }

    public class AcceptedByUserEvent : FixedAssetEvent
    {
        public AcceptedByUserEvent()
        {
            Type = FixedAssetEventType.AcceptedByUser;
        }
    }

    public class RejectedByUserEvent : FixedAssetEvent
    {
        public RejectedByUserEvent()
        {
            Type = FixedAssetEventType.RejectedByUser;
        }
    }

    public class StoredByManagerEvent : FixedAssetEvent
    {
        public StoredByManagerEvent()
        {
            Type = FixedAssetEventType.StoredByManager;
        }
    }

    public class ReturnInitiatedByManagerEvent : FixedAssetEvent
    {
        public ReturnInitiatedByManagerEvent()
        {
            Type = FixedAssetEventType.ReturnInitiatedByManager;
        }
    }
}
