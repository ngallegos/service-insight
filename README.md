# Service Insight Web
This is a Blazor app that can be used in place of the windows-only Particular.net implementation for those with mac or linux machines.


The api endpoints used were discovered by the doing a GET on a running service control instance's root (`http://{your-service-control-domain}:{your-service-control-port}/api/`), which returns a payload with many of the api endpoints used by service insight, and by referencing the official [service insight source code](https://github.com/Particular/ServiceInsight).

### Known limitations
* Viewing the logs when inspecting a message is not available
* Retrying a failed message has not been implemented
* All context actions (right clicking, hovering, etc...) on any element are not available like they are in the native windows app, most notably extra information on the various graphs and charts.

It could benefit from some better styling for usability. For example:
* The message properties table in the lower right is very crammed and hard to look at. 
* The message explorer pane doesn't restrict itself to the window size and viewing headers in particular makes you scroll the whole page instead of just the pane its supposed to show up in.

In spite of these limitations, the current state of the app has made it more than sufficient for my use cases while diagnosing and inspecting messages on a non-windows machine