// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("AsyncUsage", "AsyncFixer01:Unnecessary async/await usage", Justification = "<暫止>", Scope = "member", Target = "~M:LivestreamRecorder.DB.Models.VideoRepository.GetVideoByIdAndChannelIdAsync(System.String,System.String)~System.Threading.Tasks.Task{LivestreamRecorder.DB.Models.Video}")]
[assembly: SuppressMessage("AsyncUsage", "AsyncFixer01:Unnecessary async/await usage", Justification = "<暫止>", Scope = "member", Target = "~M:LivestreamRecorder.DB.Models.ChannelRepository.GetChannelByIdAndSourceAsync(System.String,System.String)~System.Threading.Tasks.Task{LivestreamRecorder.DB.Models.Channel}")]
[assembly: SuppressMessage("AsyncUsage", "AsyncFixer01:Unnecessary async/await usage", Justification = "<暫止>", Scope = "member", Target = "~M:LivestreamRecorder.DB.Models.UserRepository.GetByIdAsync(System.String)~System.Threading.Tasks.Task{LivestreamRecorder.DB.Models.User}")]
