import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], demoData: "", loading: true };
  }

  componentDidMount() {
      this.populateData();
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    let demodata = this.state.loading ? <p><em>Loading...</em></p> : <p><strong>{this.state.demoData}</strong></p>;

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
        <p>This is some cool demo data</p>
        {demodata}
      </div>
    );
  }

  async populateData() {
    const wresponse = await fetch('weatherforecast');
    const wdata = await wresponse.json();

    const dresponse = await fetch('demodata');
    const ddata = await dresponse.text();

    this.setState({ forecasts: wdata, demoData: ddata, loading: false });
  }
}
