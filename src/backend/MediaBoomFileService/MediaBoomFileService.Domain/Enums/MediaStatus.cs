namespace MediaBoomFileService.Domain.Enums;

/// <summary>
/// Current state of file (it is uploading, processing or something else.)
/// </summary>
public enum MediaStatus
{
    UPLOADING,
    UPLOADED,
    PROCESSING,
    READY,
    FAILED,
    DELETED
}