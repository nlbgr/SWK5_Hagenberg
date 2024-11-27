const ORDER_MANAGEMENT_BASE_URI = "https://localhost:5001";

interface Customer {
  id:      string;
  name:    string;
  zipCode: number;
  city:    string;
  rating:  string;
}

async function fetchCustomerByIdAsync(id: string): Promise<Customer> {
	if (!id) {
		throw new Error('ID must not be empty');
	}

	const response = await fetch(`${ORDER_MANAGEMENT_BASE_URI}/api/customers/${id}`, { method: "GET", headers: {"ACCEPT": "application/json"}});
	if (response.status != 200) {
		throw new Error(`Failed with status code ${response.status}`);
	}

	return await response.json();
}