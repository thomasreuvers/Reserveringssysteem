import React, { Component } from "react";

class Table extends Component {
  constructor(props) {
    super(props);
    this.state = { Reservations: [] };
  }

  componentDidMount() {
    this.fetchReservations();
    this.timer = setInterval(() => this.fetchReservations(), 5000);
  }

  componentWillUnmount() {
    clearInterval(this.timer);
    this.timer = null;
  }

  fetchReservations = async () => {
    try {
      let res = await fetch(
        `https://localhost:44381/api/reservation/getallreservations`,
        {
          method: "get",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
          },
        }
      );

      let result = await res.json();
      this.setState({
        Reservations: result.map((reservation, i) => (
          <tr key={i}>
            <td>{reservation.bookingName}</td>
            <td>{reservation.phoneNumber}</td>
            <td>{reservation.numberOfGuests}</td>
            <td>{reservation.setting === 0 ? "inside" : "outside"}</td>
            <td>{reservation.bookingDateTime}</td>
          </tr>
        )),
      });
    } catch (e) {
      console.log(e);
    }
  };

  render() {
    //Whenever our class runs, render method will be called automatically, it may have already defined in the constructor behind the scene.
    return (
      <table className="table table-dark">
        <thead>
          <tr>
            <th scope="col">Reservation name</th>
            <th scope="col">Phone number</th>
            <th scope="col">Number of guests</th>
            <th scope="col">Setting</th>
            <th scope="col">Reservation time</th>
          </tr>
        </thead>
        <tbody>{this.state.Reservations}</tbody>
      </table>
    );
  }
}

export default Table;
