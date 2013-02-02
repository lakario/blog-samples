Summary
---
This HttpModule will allow you to redirect incoming requests which match a given regular expression.

Available options
---
* targetUrl: regular expression to match incoming url (required)
* destinationUrl: redirect url, supports regex replacement (required)
* ignoreCase: ignores the url case when comparing (not required, default: false)
* permanent: issues redirect as permanent (301) instead of temporary (302) (not required, default: false)
* ignoreQuery: forces the module to exclude the querystring when making a comparison (not required, default false)

