ETL Application For Datawarehousing and Business Intelligence
=================================================================

- This `TimerTriggered` Azure Function is supposed to be triggered automatically. 

- The NCrontab Expression here ``` 59 23 * * 1-5 ``` means it will trigger at 23:59 every weekday.

The execution is as follows.

1. Initialize a Database Context during startup to connect to the database.
2. Utilize the context to pull records from the specified table.
3. Filter only the records which aren't updated and apply transformations to them.







