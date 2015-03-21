Assumptions:
-Each client send server load information along with a time stamp
-There will be limited number of clients sending load information for a server and there will be a larger number of clients fetching server statistics

Endpoints:
Get - to get server statistics
Post - to post sever load information

Limitations/ features not implemented includes but not limited to:
-No logging
-No security
-Not storing each data point
-Not using .net threadsafe collections.using locks

