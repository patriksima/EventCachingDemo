# PoC of incremental build of complex report

Incremental reporting using CQRS, MediatR and events

A common reporting approach:
- A nightly job that runs a complex query and kills the database
- a report is updated only once a day at night

The disadvantages are obvious. Large database load, potential deadlocks. Report is only updated once a day.

I've tried a different approach. Build a report incrementally each time relevant data changes using events using CQRS and MediatR.

The advantage is low database load, the report is always up to date. The disadvantage is higher code complexity and programmer requirements. Some reports may not be solvable this way.
