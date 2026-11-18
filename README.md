# Zoom

Use this Blackbird integration to manage Zoom meetings and respond to meeting activity.

<!-- begin docs -->

## Connection

Authorize the OAuth 2.0 connection with your Zoom account to use the meeting actions.

## Actions

### Meetings

- **List live meetings** Outputs the meetings currently in progress.
- **Create a meeting** Schedules a meeting for the connected user.
- **End a meeting** Ends the meeting with the specified ID.

## Events

### Meetings

- **On meeting created** Triggers when a user creates a meeting.
- **On meeting started** Triggers when a host starts a meeting.
- **On meeting ended** Triggers when a host ends a meeting.
- **On meeting deleted** Triggers when a scheduled meeting is deleted.
- **On meeting participant joined** Triggers when a participant or host joins a meeting.
- **On meeting participant left** Triggers when a participant or host leaves a meeting.
- **On chat message sent** Triggers when a user sends a public or private message in a meeting.

### Recordings

- **On recording completed** Triggers when a meeting or webinar recording becomes available.
- **On transcript completed** Triggers when a recording transcript becomes available after a meeting or webinar.

<!-- end docs -->
