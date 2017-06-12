Scenario:

Manually create two e-mail boxes

Test steps:
1.	Log in to acc1 from UI. 
2.	Create letter and send it to acc2.
3.	Check e-mail in Sent present.
4.	Log in to acc2 from UI. Check sent e-mail is present in Inbox

Perform tests for 10 different accounts

Use NUnit, NuGet, Selenium WebDriver.

Store accounts in xml, csv and any SQL DB (in all 3 data storages)

Tests should provide detailed log, make screenshots on failure and provide results in this format:

Account |	Results | (Passed\Failed) |	Failures reason
