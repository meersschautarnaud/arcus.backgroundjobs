---
title: "Arcus Background Jobs"
layout: default
permalink: /
slug: /
sidebar_label: Welcome
---

[![NuGet Badge](https://buildstats.info/nuget/Arcus.BackgroundJobs.CloudEvents?packageVersion=0.3.0)](https://www.nuget.org/packages/Arcus.BackgroundJobs.CloudEvents/0.3.0)

# Installation

The Arcus BackgroundJobs can be installed via NuGet:

```shell
PM > Install-Package Arcus.BackgroundJobs.CloudEvents -Version 0.3.0
```

For more granular packages we recommend reading the documentation.

# Features

- **General**
    - [Securely Receive CloudEvents](features/general/receive-cloudevents-job)
- **Security**
    - [Automatically invalidate cached secrets from Azure Key Vault](features/security/auto-invalidate-secrets)
- **Databricks**
    - [Measure Databricks job run outcomes as metric](features/databricks/job-metrics)
    - [Interact with Databricks to gain insights](features/databricks/gain-insights)

# License
This is licensed under The MIT License (MIT). Which means that you can use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the web application. But you always need to state that Codit is the original author of this web application.

*[Full license here](https://github.com/arcus-azure/arcus.backgroundjobs/blob/master/LICENSE)*
