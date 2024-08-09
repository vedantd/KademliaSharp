# KademliaSharp
A robust, extensible Kademlia Distributed Hash Table (DHT) implementation for .NET

## Project Status

### Implemented Features
- [x] **NodeId Implementation:**
  - XOR-based distance calculation between nodes.
- [x] **K-Bucket Data Structure:**
  - Management of nodes within k-buckets.
- [x] **Basic Routing Table Management:**
  - Organization and lookup of nodes using a routing table composed of k-buckets.
- [x] **Core DHT Operations:**
  - `FindNode`, `FindValue`, and `Store` operations for interacting with the DHT.


#### 2. **TO DO's**

- [ ] **Bucket Splitting Strategy:**
  - Implement logic for customizable bucket splitting based on different criteria.
- [ ] **Parallel Node Lookups:**
  - Implement support for parallel lookups in `FindNode` and `FindValue` operations.
- [ ] **Timeout and Retry Mechanism:**
  - Implement a configurable timeout and retry mechanism for network requests.
- [ ] **Data Expiry and Refresh:**
  - Implement logic to handle data expiry and automatic refresh of stored values.









## Contributing

Contributions are welcome! If you'd like to contribute to this project, please fork the repository and submit a pull request. For major changes, please open an issue first to discuss what you would like to change.