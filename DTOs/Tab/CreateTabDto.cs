public record CreateTabDto
(
    string Name,
    int InstrumentId,
    string Url,
    int? TuningId
);