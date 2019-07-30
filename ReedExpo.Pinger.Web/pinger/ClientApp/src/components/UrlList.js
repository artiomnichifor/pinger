﻿import React, { Component } from 'react';

export class UrlList extends Component {
    static displayName = UrlList.name;

    constructor(props) {
        super(props);
        this.state = { sites: [], loading: true };

        fetch('site/GetAllSites')
            .then(response => response.json())
            .then(data => {
                this.setState({ sites: data, loading: false });
            });
    }

    static renderSitesTable(sites) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Url</th>
                        <th>Polling Time</th>
                        <th>Expected Time</th>
                    </tr>
                </thead>
                <tbody>
                    {sites.map(sites =>
                        <tr key={sites.url}>
                            <td>{sites.url}</td>
                            <td>{sites.pollingTime}</td>
                            <td>{sites.expectedTime}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : UrlList.renderSitesTable(this.state.sites);

        return (
            <div>
                {contents}
            </div>
        );
    }
}
