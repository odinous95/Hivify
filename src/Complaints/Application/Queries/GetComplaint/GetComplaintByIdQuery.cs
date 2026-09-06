using BuildingBlocks.ApplicationPorts.Messeging;
using Complaints.Application.DTOs;

namespace Complaints.Application.Queries.GetComplaint;

public sealed record GetComplaintByIdQuery(Guid ComplaintId) : IQuery<ComplaintListItem?>;