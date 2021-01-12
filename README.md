# GymSite

Requirements:
 - dotnetcore3.1
 - mysql (barebone db skeleton is in `dump-latest.sql`)

TODO: 
 - ~~Change DB table name `TrainingGroups` to `TrainGroups`.~~ - *Done.*
 - ~~Assign clients to trainings.~~ - *Done.*
 - ~~Check for "Single" groups~~ - *Done. Added property `MaxClients` to model `Price`*
 - ~~Add simple authentication method~~ - *Done.*
 - ~~**Known bug**: Can assign to wrong group if add new training from `ClientInfo` page.~~
 - Add user management if user has admin role (single page atm)
  