# PoC: Incremental building of a complex report

Building report slice-by-slice using CQRS, MediatR and events

Common approach to reporting:
- a nightly task runs a complex query and kills the database :)
- report is updated only once a day

Disadvantages are obvious. Heavy database load, potential deadlocks. Report is updated only once a day.

I tried a different approach. Building the report incrementally whenever relevant data changes using events, CQRS and MediatR.

The advantage is low database load, the report is always up to date. The disadvantage is higher code complexity and programmer requirements. Some reports may not be solvable in this way.
