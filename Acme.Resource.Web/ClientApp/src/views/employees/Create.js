import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';
import { ArrowClockwise } from 'react-bootstrap-icons';

export class EmployeeCreate extends Component {
	static displayName = EmployeeCreate.name;

	constructor(props) {
		super(props);
		this.state = {
			fullName: '',
			birthdate: '',
			tin: '',
			typeId: 1,
			profileImage: '',
			loading: false,
			loadingSave: false,
			isSubmitted: false,
			validationErrors: {}
		};
	}

	componentDidMount() {
	}

	handleChange(event) {
		this.setState({ [event.target.name]: event.target.value });
	}

	handleSubmit(e) {
		e.preventDefault();
		this.setState({ validationErrors: {}, isSubmitted: false });
		if (window.confirm("Are you sure you want to save?")) {
			const errors = this.validate(this.state)
			this.setState({ validationErrors: errors, isSubmitted: true }, function () {
				if (Object.keys(this.state.validationErrors).length === 0 && this.state.isSubmitted) {
					this.saveEmployee();
				}
			});
		}
	}

	render() {
		let contents = this.state.loading
			? <p><em>Loading...</em></p>
			: <div>
				<form>
					<div className='form-row'>
						<div className='form-group col-md-6'>
							{this.state.profileImage
								? <img src={this.state.profileImage} className='profile' alt={this.state.fullName} height={150} width={150} />
								: <img src={'../assets/blank-avatar.jpg'} className='profile' alt='Empty' height={150} width={150} />
							}
							<ArrowClockwise onClick={() => this.refreshImage()}
								className='refresh-image'
								title='Refresh Image'
								color="royalblue"
								size={24} />
						</div>
						<div className='form-group col-md-6'>
						</div>
					</div>
					<div className='form-row'>
						<div className='form-group col-md-6'>
							<label htmlFor='inputFullName4'>Full Name: *</label>
							<input type='text' className='form-control' id='inputFullName4' onChange={this.handleChange.bind(this)} name="fullName" value={this.state.fullName} placeholder='Full Name' />
							{this.state.validationErrors.fullName
								? <span>{this.state.validationErrors.fullName}</span>
								: <span></span>
							}
						</div>
						<div className='form-group col-md-6'>
							<label htmlFor='inputBirthdate4'>Birthdate: *</label>
							<input type='date' className='form-control' id='inputBirthdate4' onChange={this.handleChange.bind(this)} name="birthdate" value={this.state.birthdate} placeholder='Birthdate' />
							{this.state.validationErrors.birthdate
								? <span>{this.state.validationErrors.birthdate}</span>
								: <span></span>
							}
						</div>
					</div>
					<div className="form-row">
						<div className='form-group col-md-6'>
							<label htmlFor='inputTin4'>TIN: *</label>
							<input type='text' className='form-control' id='inputTin4' onChange={this.handleChange.bind(this)} value={this.state.tin} name="tin" placeholder='TIN' />
							{this.state.validationErrors.tin
								? <span>{this.state.validationErrors.tin}</span>
								: <span></span>
							}
						</div>
						<div className='form-group col-md-6'>
							<label htmlFor='inputEmployeeType4'>Employee Type: *</label>
							<select id='inputEmployeeType4' onChange={this.handleChange.bind(this)} value={this.state.typeId} name="typeId" className='form-control'>
								<option value='1'>Regular</option>
								<option value='2'>Contractual</option>
							</select>
							{this.state.validationErrors.typeId
								? <span>{this.state.validationErrors.typeId}</span>
								: <span></span>
							}
						</div>
					</div>
					<button type="submit" onClick={this.handleSubmit.bind(this)} disabled={this.state.loadingSave} className="btn btn-primary mr-2">{this.state.loadingSave ? "Loading..." : "Save"}</button>
					<button type="button" onClick={() => this.props.history.push("/employees/index")} className="btn btn-primary">Back</button>
					<button type="button" onClick={() => this.generateFakeData()} className="btn btn-primary pull-right">Generate</button>
				</form>
			</div>;

		return (
			<div>
				<h1 id="tabelLabel">Add Employee</h1>
				<p>* Required Fields</p>
				{contents}
			</div>
		);
	}

	validate = (formValues) => {
		const fieldErrors = {};
		if (!formValues.fullName) {
			fieldErrors.fullName = "Full Name is required"
		}

		if (!formValues.tin) {
			fieldErrors.tin = "TIN is required"
		}

		if (!formValues.birthdate) {
			fieldErrors.birthdate = "Birthdate is required"
		}

		if (formValues.typeId === 0) {
			fieldErrors.employeeType = "Employee type is required"
		}
		return fieldErrors;
	};

	async saveEmployee() {
		this.setState({ loadingSave: true });
		const token = await authService.getAccessToken();
		const requestOptions = {
			method: 'POST',
			headers: !token ? {} : { 'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json' },
			body: JSON.stringify(this.state)
		};
		const response = await fetch('api/employees', requestOptions);

		if (response.status === 201) {
			this.setState({ loadingSave: false });
			alert("Employee successfully saved");
			this.props.history.push("/employees/index");
		}
		else {
			alert("There was an error occured.");
		}
	}

	async generateFakeData() {
		const token = await authService.getAccessToken();
		const response = await fetch('api/employees/generate', {
			headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
		});
		const data = await response.json();
		this.setState({
			fullName: data.fullName,
			birthdate: data.birthdate,
			tin: data.tin,
			typeId: data.typeId,
			profileImage: data.profileImage,
		});
	}

	async refreshImage() {
		const token = await authService.getAccessToken();
		const response = await fetch('api/employees/refresh-image', {
			headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
		});
		const data = await response.json();
		this.setState({ profileImage: data.profileImage });
	}
}
