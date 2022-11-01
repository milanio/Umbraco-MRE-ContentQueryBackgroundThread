This repository represents a minimal reproducible example (MRE) for an issue https://github.com/umbraco/Umbraco-CMS/issues/13334

# Setup

1. Import bacpac file `umbraco-cms-test-bck-thread.bacpac` (available in the root of the repo folder) into your local SQL database
2. Ensure you have a breakpoint somewhere inside `SyncTask.PerformExecuteAsync`
3. Start debugging session in Visual Studio 
4. Within 10 seconds of the website `SyncTask.PerformExecuteAsync` will be called and your breakpoint will be hit
