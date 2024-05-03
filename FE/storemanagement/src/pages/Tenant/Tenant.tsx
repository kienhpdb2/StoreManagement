import React, { useEffect, useState } from "react";
import { Button, Col, Row, Input, Space, Table, message, Typography } from "antd";
import { SearchOutlined, InfoCircleOutlined, FormOutlined } from '@ant-design/icons';
import type { ColumnsType } from 'antd/es/table';
import moment from "moment";
import TenantDto from 'pages/Tenant/models/TenantDto';
import axios from '../../common/baseAxios';
import ViewModal from "./View";
import AddModal from "./Add";
import EditModal from "./Edit";
const Tenant = () => {
    const [data, setData] = useState(new Array<TenantDto>);
    const [open, setOpen] = useState(false);
    const [openView, setOpenView] = useState(false);
    const [openEdit, setOpenEdit] = useState(false);
    const [curentId, setCurentId] = useState<any>(Number);
    const [curentTenant, setCurentTenant] = useState<any>();
    const columns: ColumnsType<TenantDto> = [
        {
            title: 'Tên Cửa Hàng',
            dataIndex: 'name',
            key: 'name'
        },
        {
            title: 'Số Điện Thoại',
            dataIndex: 'phone',
            key: 'phone'
        },
        {
            title: 'E-mail',
            dataIndex: 'email',
            key: 'email'
        },
        {
            title: 'Người Đại Diện',
            dataIndex: 'storeOwnerName',
            key: 'storeOwnerName'
        },
        {
            title: 'Ngày tạo',
            dataIndex: 'createdAt',
            key: 'createdAt',
            render: (createdAt: Date) => (
                <Space size="middle">{moment(createdAt).format("DD/MM/YYYY hh:mm")}</Space>
            )
        },
        {
            title: 'Trạng Thái',
            dataIndex: 'isActived',
            key: 'isActived',
            render: (isActived: boolean) => (
                <Space size="middle">{ isActived ? <Typography.Text type="success">Đang Hoạt Động</Typography.Text > : <Typography.Text type="danger">Khóa</Typography.Text>}</Space>
            )
        },
        {
            title: ``,
            key: `action`,
            render: (record) => (
                <Space size="middle">
                    <Button size="middle" onClick={() => handleFormView(record.id)}><InfoCircleOutlined /></Button>
                    <Button size="middle" onClick={() => handleFormEdit(record.id)}><FormOutlined /></Button>
                </Space>
            )
        }
    ]
    useEffect(() => {
        getListTenant();
    }, [])
    const getListTenant = (search: string = '') => {
        axios.get(`Tenant`).then((res) => {
            let { data } = res;
            if (data.success) {
                setData(data.data);
            }
        })
    }
    const closeForm = (isSave = false) => {
        setOpen(false);
        setOpenView(false);
        setOpenEdit(false);
        if (isSave) {
            getListTenant();
        }
    }
    const handleFormView = (id: any) => {
        setCurentId(id);
        setOpenView(true);
    }
    const handleFormEdit = (id: any) => {
        axios.get(`Tenant/${id}`).then((res) => {
            let { data } = res;
            if (data.success) {
                setCurentTenant(data.data);
                setOpenEdit(true);
            }
            else {
                message.error(data.error)
            }

        })
    }
    const handleOnChange = (event: any) => {
        getListTenant(event.target.value);
    }
    return <>
        <div>
            <Row>
                <Col span={24} style={{ fontWeight: 700, fontSize: '23px' }}>QUẢN LÝ CỬA HÀNG</Col>
            </Row>
            <Row style={{ marginTop: '20px' }}>
                <Col span={12}>
                    <Input onChange={handleOnChange} placeholder="Tìm kiếm theo tên hoặc email" prefix={<SearchOutlined />} />
                </Col>
                <Col span={12} style={{ textAlign: 'right' }}>
                    <Button onClick={() => setOpen(true)}>Thêm Mới</Button>
                </Col>
            </Row>
            <Row style={{ marginTop: '20px' }}>
                <Col span={24}>
                    <Table columns={columns} dataSource={data} rowKey="id" ></Table>
                </Col>
            </Row>
        </div>
        {open && <AddModal open={open} closeForm={closeForm} />}
        {openView && <ViewModal open={openView} closeForm={closeForm} id={curentId} />}
        {openEdit && <EditModal open={openEdit} closeForm={closeForm} curentTenant={curentTenant} />}
    </>
}
export default Tenant;