# This project is for my **_Datawarehousing and Business Intelligence_** course as part of UC. 

*Primary intention was to create a POC where data can be moved around to different cloud systems.*

Project Arch:

![image](https://github.com/Neranjan98/DWBI/assets/47419937/43ae2953-876b-4405-bb5e-66291f5a73b2)

1. The Ingestion is taken care by a .NET Console App which pulls data from [Washington Data Portal](https://data.wa.gov/Transportation/Electric-Vehicle-Population-Data/f6w7-q2d2/about_data)
2. It is connected to SQL Server Database instance on AWS RDS, using Entity Framework. It ingests data in a single shot.
3. A Databricks community instance is spun up, which connects to the RDS instance, modify the data and write it back to the RDS instance.
4. Finally, [Preset, Cloud Instance of Apache Superset](https://preset.io/) is used to create a Dashboard which sits on top of the RDS instance to create charts.
