import React, { Component } from 'react';
import { Container } from 'reactstrap';


export class UrlForm extends Component {
    static displayName = UrlForm.name;
    constructor(props) {
        super(props);
        this.state = { items: [], url: '', expectedTime: '', pollingTime: '' };
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);


    }

    render() {
        return (
            <div>
                <TodoList items={this.state.items} />

                <form onSubmit={this.handleSubmit}>
                    <label htmlFor="new-url">
                        <input
                            id="new-url"
                            type="text"
                            onChange={this.handleChange}
                            value={this.state.url}
                            onChange={this.handleChange('url')}
                        />
                    </label>
                    <label htmlFor="new-expectedTime">
                        <input
                            id="new-expectedTime"
                            type="number"
                            onChange={this.handleChange}
                            value={this.state.expectedTime}
                            onChange={this.handleChange('expectedTime')}
                        />
                    </label>
                    <label htmlFor="new-pollingTime">
                        <input
                            id="new-pollingTime"
                            type="number"
                            onChange={this.handleChange}
                            value={this.state.pollingTime}
                            onChange={this.handleChange('pollingTime')}
                        />
                    </label>

                    <input type="submit" value="Submit" />
                </form>
            </div>
        );
    }

    handleChange(key) {
        return function (e) {
            var state = {};
            state[key] = e.target.value;
            this.setState(state);
        }.bind(this);

    }

    handleSubmit(e) {
        e.preventDefault();
        if (!this.state.url.length) {
            return;
        }
        const newItem = {
            url: this.state.url,
            expectedTime: this.state.expectedTime,
            pollingTime: this.state.pollingTime,
            id: Date.now()
        };
        this.setState(state => ({
            items: state.items.concat(newItem)
        }));

        fetch('site/CreateUrl', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                url: newItem.url,
                expectedTime: newItem.pollingTime,
                pollingTime: newItem.expectedTime
            })
        }
        )

    }
}

class TodoList extends React.Component {
    render() {
        return (
            <ul>
                {this.props.items.map(item => (
                    <li key={item.id}>{item.url}>{item.pollingTime}>{item.expectedTime}</li>
                ))}
            </ul>
        );
    }
}

